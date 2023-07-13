

using PrepareVideos;

var worker = new PrepareVideosWorker("http://localhost:8080/api/", "prepare_videos");

await worker.Polling();