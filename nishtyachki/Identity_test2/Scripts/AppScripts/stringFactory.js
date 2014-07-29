function stringFactory(str) {

    this.str = str;
    
    var resource = "Resources/";
    
    this.bucketBlock = str + resource + "bucket_block.jpg";
    this.bucket = str + resource + "bucket.jpg";
    this.bucketInUse = str + resource + "bucket_in_using.jpg";
    this.buttonGreen = str + resource + "green_button.png";
    this.buttonRed = str + resource + "red_button.jpg";

    this.webApi = function () {
        this.QueueDataService = function () {
            this.getQueue = str + "api/queue";
            this.delete = str + "api/queue/delete";
            this.changeStat = str + "api/queue/change";
            this.blockUnblock = str + "api/queue/block";
        };
        this.DataService = function () {
            this.getNisht = str + "api/nisht";
            this.update = str + "api/nisht/add";
            this.delete = str + "api/nisht/delete";
            this.changeStat = str + "api/nisht/change";
        };
    };

}