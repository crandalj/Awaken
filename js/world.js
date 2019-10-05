import Player from "../../js/player.js";

// Extends Phaser.Scene and has core logic for the world
export default class World extends Phaser.Scene {
    constructor(){
        super({
            key: "World"
        });
    }
    preload() {
        this.load.spritesheet(
            "player",
            "../assets/images/player.png",
            {
                frameWidth: 32,
                frameHeight: 32,
                margin: 0,
                spacing: 0
            }
        );
        this.load.image(
            "tiles",
            "../assets/images/gametiles.png"
        );
        this.load.tilemapTiledJSON(
            "map",
            "../assets/maps/gameworld.json"
        );
    }

    create(){
        const map = this.make.tilemap({key: "map"});
        const tiles = map.addTilesetImage(
            "gametiles",
            "tiles"
        );

        map.createDynamicLayer("Background", tiles);
        this.wallsLayer = map.createDynamicLayer("Walls", tiles);

        // Instantiate player with spawn point
        const spawnPoint = map.findObject(
            "Objects",
            obj => obj.name === "spawnpoint"
        );
        this.player = new Player(this, spawnPoint.x, spawnPoint.y);

        // Collide player with tiles in Walls Layer
        this.wallsLayer.setCollisionByProperty({collides: true});
        this.physics.world.addCollider(this.player.sprite, this.wallsLayer);

        this.cameras.main.startFollow(this.player.sprite);
        this.cameras.main.setBounds(0, 0, map.widthInPixels, map.heightInPixels);
    }

    update(time, delta){
        // Attach player input (movement, animations, etc)
        this.player.update();
    }
}