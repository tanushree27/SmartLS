# SmartLS (Smart Localsearch)

Welcome to Smart Localsearch application for Windows.

This Application (C#, WPF, Visual Studio) helps to search local documents using [Elasticsearch](https://www.elastic.co/products/elasticsearch). The application can also take an image as query (an image with text, this text is extracted using [Tesseract OCR](https://github.com/tesseract-ocr/tesseract) and is used as query).

## Running SmartLS

* [Setup Elasticsearch 5.3.2](#setup-elasticsearch)
* [Setup FSCrawler 2.3 SNAPSHOT](#setup-fscrawler)

### Setup Elasticsearch

* Download [Elasticsearch](https://www.elastic.co/downloads/elasticsearch)
* Unzip and run `bin\elasticsearch.bat`.
* [Check](http://localhost:9200/) your Elasticsearch is up and running.

### Setup FSCrawler

* Download [FSCrawler 2.3 SNAPSHOT](https://oss.sonatype.org/content/repositories/snapshots/fr/pilato/elasticsearch/crawler/fscrawler/2.3-SNAPSHOT/fscrawler-2.3-20170426.162252-28.zip)
* Setup `JAVA_HOME` Variable (**Java 1.8.** or later)
* In Command prompt reach the `\bin` directory and create a job by
```sh
fscrawler.bat project
```
* Open `C:\Users\$username$\.fscrawler\project\_settings.json` and change `"url" : "tmp/es"` to
```sh
"url" : "C:/Documents",
```
* Create a Directory Documents in C drive, and load it with files which you want to index.
* Again run fscrawler
```sh
fscrawler.bat project
```
* The FSCrawler will automatically index all the documents in the folder in elasticsearch under `"index": "project"` and `"type": "Doc"`.
* By default FSCrawler scans the directory every 15 mins for any changes and updates the index accordingly.

For more info visit [FSCrawler](https://github.com/dadoonet/fscrawler).


# Team
* [Tanushree Tumane](https://github.com/tanushree27)
* [Yash Bhatambare](https://github.com/yashb5)
