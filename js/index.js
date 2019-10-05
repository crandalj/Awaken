// Author: Joseph Crandal, dungeonengineer.com

import World from "./world.js";

const config = {
    type: Phaser.AUTO,
    width: 800,
    height: 600,
    parent: "game-container",
    pixelArt: true,
    backgroundColor: "#000000",
    scene: World,
    physics: {
        default: "arcade",
        arcade: {
            gravity: {y: 0}
        }
    }
};

const game = new Phaser.Game(config);