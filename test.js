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

const rl = readline.createInterface({
	    input: process.stdin,
	    output: process.stdout
});
rl.on('line', function (input) {
    if (input === 'quit()') {
        rl.close();
    } else if (input === 'w') {
        l298n.forward(l298n.NO1);
        l298n.forward(l298n.NO2);
    } else if (input === 'a') {
        l298n.backward(l298n.NO1);
        l298n.forward(l298n.NO2);
    } else if (input === 'd') {
        l298n.forward(l298n.NO1);
        l298n.backward(l298n.NO2);
    } else if (input === 'x') {
        l298n.backward(l298n.NO1)
        l298n.backward(l298n.NO2)
    } else if (input === 's') {
        l298n.stop(l298n.NO1);
        l298n.stop(l298n.NO2);
    } 
    
    
    else if (input === 'y') {
        left_motor_motion = !left_motor_motion;
        if (left_motor_motion){left_motor.servoWrite(1000);}
        else{left_motor.servoWrite(1800);}
    } else if (input === 'u') {
        right_motor_motion = !right_motor_motion;
        if (right_motor_motion){right_motor.servoWrite(1000);}
        else{right_motor.servoWrite(500);}
    } else if (input === 'i') {
        bottom_motor_motion = !bottom_motor_motion;
        if (bottom_motor_motion){bottom_motor.servoWrite(1000);}
        else{bottom_motor.servoWrite(500);}
    } else {
        l298n.setSpeed(l298n.NO1,parseInt(input));
        l298n.setSpeed(l298n.NO2,parseInt(input));
    }
});

process.on("SIGINT", function(){
    l298n.stop(l298n.NO1);
    l298n.stop(l298n.NO2);
    console.log('shutdown!');
    process.exit(0);
});
