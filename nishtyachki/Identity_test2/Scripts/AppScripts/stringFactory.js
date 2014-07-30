function stringFactory(str) {

    this.str = str;
    
    var resource = "Resources/";
    
    this.bucketBlock = str + resource + "bucket_block.jpg";
    this.bucket = str + resource + "bucket.jpg";
    this.bucketInUse = str + resource + "bucket_in_using.jpg";
    this.buttonGreen = str + resource + "green_button.png";
    this.buttonRed = str + resource + "red_button.jpg";

    this.getImg = function (img) {
        return str + resource + img;
    }

    this.getWebApiControll = function (api) {
        return str + api;
    }

    this.getQueue = function (control) {
        return str + "api/" + control + "/queue";
    };

    this.delete = function (control) {
        return str + "api/" + control + "/delete";
    };

    this.changeStat = function (control) {
        return str + "api/" + control + "/change";
    };

    this.blockUnblock = function (control) {
        return str + "api/" + control + "/block";
    };

    this.getNisht = function (control) {
        return str + "api/" + control;
    };

    this.update = function (control) {
        return str + "api/" + control + "/add";
    };
}