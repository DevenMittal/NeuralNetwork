using NeuralNetwork;
using System.Globalization;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var net = new NeuralNet(new ActivationFunction(Math.Tanh, x => 1 - Math.Pow(Math.Tanh(x), 2)), new ErrorFunction((o,t) => Math.Pow(t-o,2), (o,t) => -2*(t-o)), 2,2,1);
net.Randomize(new Random(42), -1, 1);
double[][] inputs = {new double[]{0,0},new double[]{1,0},new double[]{0,1},new double[]{1,1}};
double[][] targets = {new double[]{0},new double[]{1},new double[]{1},new double[]{0}};
Console.WriteLine("step,p00,p10,p01,p11,sum_squared_error");
for(int step=0;step<=100000;step++) {
 if(step % 1000 == 0) {
  var predictions = inputs.Select(x => net.Compute(x)[0]).ToArray();
  double error = inputs.Select((x,i) => net.GetError(x,targets[i])).Sum();
  Console.WriteLine(step+","+string.Join(",",predictions.Select(x=>x.ToString("R")))+","+error.ToString("R"));
 }
 if(step<100000) net.TrainGradientDescent(inputs,targets,0.01);
}
