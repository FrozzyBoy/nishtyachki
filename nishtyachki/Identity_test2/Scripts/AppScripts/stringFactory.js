function stringFactory(str) {

    this.str = str;
    
    var resource = "Resources/";
    
    this.getImg = function (img) {
        return str + resource + img;
    }

    this.getWebApiControll = function (api) {
        return this.str + api;
    }

}