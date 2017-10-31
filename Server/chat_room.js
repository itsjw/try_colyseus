'use strict'
var Room = require('colyseus').Room;

class ChatRoom extends Room {

  constructor () {
    super();

    this.setState({
      players: {},
      messages: [],
      GameRoom: []
    });
  }

  onInit (options) {
    this.options = options;

    this.setPatchRate( 1000 / 20 );
    this.setSimulationInterval( this.update.bind(this) );

    console.log("***ChatRoom created!***", options);


  }

  requestJoin (options) {

    console.log("***request join***!", options);
//
    console.log("players.length: "+Object.keys(this.state.players).length);
    console.log("clients.length: "+Object.keys(this.clients).length);


    console.log("*** END OF Request Join ***");

    return Object.keys(this.state.players).length < this.options.maxClients;

 

    //return true;
  }


  onJoin (client) {

  console.log("*** onJoin ***");


    console.log("client joined!", client.sessionId);
    console.log("client joined!", "Object.keys(this.state.clients).length: " + Object.keys(this.state.players).length);


    this.state.players[client.sessionId] = { x: 0, y: 0 };

    var roomname = {
      roomName : 'a room passed as string'

    }

      var text = JSON.stringify (roomname);
      //var myVar = JSON.parse(text);

      console.log("stringify text: " + text);

     //this.state.roomname.push ('a room passed as string');
           this.state.GameRoom.push (text);


console.log("*** END Of onJoin");
  }

  onLeave (client) {
    console.log("client left!", client.sessionId);
    delete this.state.players[client.sessionId];
  }

  onMessage (client, data) {
    //console.log(data, "received from", client.sessionId);
    this.state.messages.push(client.sessionId + " sent " + data);
    
  }

  update () {
   // console.log("num clients:", Object.keys(this.clients).length);
    // for (var sessionId in this.state.players) {
    //   this.state.players[sessionId].x += 0.0001;
    // }
  }

  onDispose () {
    console.log("Dispose ChatRoom");
  }




}

module.exports = ChatRoom;
