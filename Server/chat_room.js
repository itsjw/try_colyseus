'use strict'
var Room = require('colyseus').Room;

class ChatRoom extends Room {

  constructor () {
    super();

    this.setState({
      players: {},
      messages: [],
      roomname: []
    });
  }

  onInit (options) {
    this.options = options;

    this.setPatchRate( 1000 / 20 );
    this.setSimulationInterval( this.update.bind(this) );

    console.log("***ChatRoom created!***", options);


  }

  requestJoin (options) {
    console.log("request join!", options);
//
    console.log("players.length: "+Object.keys(this.state.players).length);
    console.log("clients.length: "+Object.keys(this.clients).length);

    return Object.keys(this.state.players).length < this.options.maxClients;

 

    //return true;
  }


  onJoin (client) {
    console.log("client joined!", client.sessionId);
    console.log("client joined!", "Object.keys(this.state.clients).length: " + Object.keys(this.state.players).length);


    this.state.players[client.sessionId] = { x: 0, y: 0 };
   //this.state.roomname.push("roomname" + Math.floor((Math.random() * 100) + 1));

   var nameOfRoom = {
    roomName : Object.keys(this.state.players).length

   }


   this.state.roomname.push(nameOfRoom);

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
