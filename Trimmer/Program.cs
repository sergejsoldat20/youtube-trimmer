// See https://aka.ms/new-console-template for more information
using Trimmer;

var worker = new TrimmerWorker(" http://192.168.101.17:8082/api/", "trimmer");

await worker.Polling();