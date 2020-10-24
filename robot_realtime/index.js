const config = require('./config.js');

var firebase = require('firebase');
// var app = firebase.initializeApp(config);
firebase.initializeApp(config);
// firebase.analytics();

var isMovement = false;
var movementVal = 0;
var isMotor1 = false;
var isMotor2 = false;
var isMotor3 = false;


//-------------
const Gpio = require('pigpio').Gpio;
const readline = require('readline');
const L298N = require('./node_modules/pigpio-l298n/l298n.js');

/////////     servo motor      ////////////
const left_motor = new Gpio(16, {mode: Gpio.OUTPUT});
const right_motor = new Gpio(20, {mode: Gpio.OUTPUT});
const bottom_motor = new Gpio(21, {mode: Gpio.OUTPUT});

// true for close, false for open
var left_motor_motion = true; 
var right_motor_motion = true;
var bottom_motor_motion = true;
/////////     ^^^^^^^^^^^^      ////////////

/////////     wheel motor      ////////////
let l298n = new L298N(25,23,24,22,17,27);
l298n.setSpeed(l298n.NO1,60);
l298n.setSpeed(l298n.NO2,60);
/////////     ^^^^^^^^^^^^      ////////////
//--------------------------


// firebase.database().ref('player1/movement').once('value').then( function(snapshot) {
//     var snap = JSON.stringify(snapshot.val())
//     console.log("SNAP: " + snap);
//     isMovement = true;
// });

firebase.database().ref('player1/movement').on('value', function(snapshot) {
    var snap = snapshot.val();
    movementVal = snap;
    console.log("SNAP: " + snap);

    if(snap == "STOP") {
        isMovement = false;
    } else {
        isMovement = true;
    }
});

firebase.database().ref('player1/motor1').on('value', function(snapshot) {
    var snap = snapshot.val();

    if(snap == "OPEN") {
        isMotor1 = true;
        moveMotor1(isMotor1);
    } else {
        isMotor1 = false;
        moveMotor1(isMotor1);
        console.log("MOTOR1: " + snap);
    }    
});

firebase.database().ref('player1/motor2').on('value', function(snapshot) {
    var snap = snapshot.val();

    if(snap == "OPEN") {
        isMotor2 = true;
        moveMotor2(isMotor2);
    } else {
        isMotor2 = false;
        moveMotor2(isMotor2);
        console.log("MOTOR2: " + snap);
    } 
});

firebase.database().ref('player1/motor3').on('value', function(snapshot) {
    var snap = snapshot.val()

    if(snap == "OPEN") {
        isMotor3 = true;
        moveMotor3(isMotor3);
    } else {
        isMotor3 = false;
        moveMotor3(isMotor3);
        console.log("MOTOR3: " + snap);
    } 
});


// This is while look, it will run forever
setInterval(() => {
    if(isMovement) {
        if (movementVal === 'W') {
            l298n.forward(l298n.NO1);
            l298n.forward(l298n.NO2);
        } else if (movementVal === 'A') {
            l298n.backward(l298n.NO1);
            l298n.forward(l298n.NO2);
        } else if (movementVal === 'D') {
            l298n.forward(l298n.NO1);
            l298n.backward(l298n.NO2);
        } else if (movementVal === 'S') {
            l298n.backward(l298n.NO1)
            l298n.backward(l298n.NO2)
        }
    }
    else{
        l298n.stop(l298n.NO1);
        l298n.stop(l298n.NO2);
    }
}, 10);


function moveMotor1(isOpen) {
    if (isOpen){left_motor.servoWrite(1000);}
    else{left_motor.servoWrite(1800);}
}

function moveMotor2(isOpen) {
    // if (isOpen){right_motor.servoWrite(1000);}
    // else{right_motor.servoWrite(500);}
}

function moveMotor3(isOpen) {
    if (isOpen){bottom_motor.servoWrite(1000);}
    else{bottom_motor.servoWrite(500);}
}