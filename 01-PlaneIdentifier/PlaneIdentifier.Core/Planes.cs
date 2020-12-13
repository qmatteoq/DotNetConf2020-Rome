using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Storage.Streams;

namespace PlaneIdentifier
{

    public sealed class PlanesInput
    {
        public ImageFeatureValue data; // BitmapPixelFormat: Bgra8, BitmapAlphaMode: Premultiplied, width: 227, height: 227
    }

    public sealed class PlanesOutput
    {
        public TensorString classLabel; // shape(-1,1)
        public IList<IDictionary<string, float>> loss;
    }

    public sealed class PlanesModel
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static async Task<PlanesModel> CreateFromStreamAsync(IRandomAccessStreamReference stream)
        {
            PlanesModel learningModel = new PlanesModel();
            learningModel.model = await LearningModel.LoadFromStreamAsync(stream);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<PlanesOutput> EvaluateAsync(PlanesInput input)
        {
            binding.Bind("data", input.data);
            var result = await session.EvaluateAsync(binding, "0");
            Debug.WriteLine(result.Outputs.Count);
            var output = new PlanesOutput();
            output.classLabel = result.Outputs["classLabel"] as TensorString;
            output.loss = result.Outputs["loss"] as IList<IDictionary<string, float>>;
            return output;
        }
    }
}
