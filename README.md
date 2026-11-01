# Neural Network from Scratch

I implemented a feedforward neural network in C#, including neurons, weighted connections, activation functions, and backpropagation. The console example learns XOR; a separate Windows Forms example draws a learned approximation of a sine wave.

## A local XOR example

The console example uses a **2 → 2 → 1** network, `tanh` activations, squared error, and a learning rate of `0.01`.

| Input | XOR target | Observed output |
| --- | --- | --- |
| `(0, 0)` | 0 | 0.000 |
| `(0, 1)` | 1 | 0.997 |
| `(1, 0)` | 1 | 0.997 |
| `(1, 1)` | 0 | 0.000 |

These are rounded values from one short local run of the original console program. Initialization is random, so this is an example rather than a repeatability or accuracy claim. The console rounds its error display; `0` there does not imply mathematically zero loss. The observed values are also in [CSV form](docs/xor-example.csv).

## Run the console example

The console project targets **.NET 6** and declares no third-party package dependencies. Run this project directly rather than building the whole solution, which also contains Windows GUI projects.

```sh
dotnet run --project "NeuralNetwork/NeuralNetwork.csproj" -c Release
```

The program continuously trains and refreshes the four outputs. Press **Ctrl+C** to stop. Both the Release build and this console training loop were checked locally on macOS.

## Sine-wave visualization

`GraphSin` is a Windows Forms project targeting `net6.0-windows`. It trains a **1 → 10 → 10 → 10 → 10 → 1** network against 70 sine samples and draws predictions on a timer.

On Windows with the matching .NET desktop tooling:

```sh
dotnet run --project "GraphSin/GraphSin.csproj" -c Release
```

This UI was inspected in source, not rerun on Windows. The older `Graph/` application also targets the Windows desktop and .NET Framework.

## Implementation

| File in `NeuralNetwork/` | Responsibility |
| --- | --- |
| `NeuralNet.cs` | Forward pass, error calculation, training loop |
| `Layer.cs` | Layer operations and outputs |
| `Neuron.cs`, `Dendrite.cs` | Activations, deltas, connection updates |
| `ActivationFunction.cs` | Activation and error functions |
| `GeneticAlgorithm.cs` | Separate crossover/mutation experiments |
| `Program.cs` | XOR example |

The sine example currently uses gradient descent; its genetic-learning loop is commented out. This repository is an experiment, without model serialization, automated gradient checks, or a benchmark suite.
