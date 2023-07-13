

using Downloader;

var downloader = new DownloaderWorker("http://localhost:8080/api/", "downloader");

await downloader.Polling();