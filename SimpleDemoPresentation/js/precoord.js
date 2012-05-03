var PreCoordClient = function(url) {
    var self = this;
    var ws = new WebSocket(url);
    var isconnected = false;
    
    ws.onopen = function(){
        console.log('onopen');
        self.isconnected = true;
    };
    
    ws.onclose = function(){
        console.log('onclose');
        self.isconnected = false;
    };
    
    ws.onmessage = function(message){
        var event = JSON.parse(message.data);
        self['on' + event.Name](JSON.parse(event.Data));
    };
    
    self.bind = function(event, handler){
        self['on' + event] = handler;
    };
    
    var waitforconnection = function(onconnected) {
        if(self.isconnected) {
            onconnected();
            return;
        }
            
        var t = setTimeout(function() {
            clearTimeout(t);            
            return waitforconnection(onconnected);
        }, 1000);        
    };
    
    var execute = function(cmdName, cmd) {
        var doc = {Action: cmdName, Data: cmd};
        ws.send(JSON.stringify(doc));
    };
    
    self.startorjoinpresentation = function(name) {
        waitforconnection(function(){
            execute('startorjoinpresentation', {Name: name});
        });        
    };
    
    self.changeslide = function(slide) {
        execute('changeslide', {NewSlide: slide});
    };
};