// See https://aka.ms/new-console-template for more information
using Trimmer;

var worker = new TrimmerWorker("http://localhost:8080/api/", "trimmer");

await worker.Polling();