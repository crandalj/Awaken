// Player class handles 2D topdown player logic. Create, animate, move.

export default class Player{
    constructor(scene, x, y){
        this.scene = scene;
    
        // Create animations
        const anims = scene.anims;
        anims.create({
            key: "player-idle",
            frames: anims.generateFrameNumbers("player", {start: 0, end: 3}),
            frameRate: 3,
            repeat: -1
        });
        //TODO: add animation for left, right, up, down, attack, hurt, dead

        // Create the player sprite that will be moved
        this.sprite = scene.physics.add
            .sprite(x, y, "player", 0)
            .setSize(32, 32)

        // Track arrow keys & WASD
        const { LEFT, RIGHT, UP, DOWN, W, A, S, D } = Phaser.Input.Keyboard.KeyCodes;
        this.keys = scene.input.keyboard.addKeys({
            left: LEFT,
            right: RIGHT,
            up: UP,
            down: DOWN,
            w: W,
            a: A,
            s: S,
            d: D
        });
    }

    update(){
        const keys = this.keys;
        const sprite = this.sprite;
        const acceleration = 200;
        
        // Apply horizontal acceleration if left/a or right/d are pressed
        if(keys.left.isDown || keys.a.isDown){
            sprite.setAccelerationX(-acceleration);
            sprite.setFlipX(true);
        } else if (keys.right.isDown || keys.d.isDown){
            sprite.setAccelerationX(acceleration);
            sprite.setFlipX(false);
        } else{
            sprite.setAccelerationX(0);
            sprite.body.velocity.x = 0;
        }

        // Apply vertical acceleration if up/w or down/s are pressed
        if (keys.up.isDown || keys.w.isDown) {
            sprite.setAccelerationY(-acceleration);
        } else if (keys.down.isDown || keys.s.isDown) {
            sprite.setAccelerationY(acceleration);
        } else {
            sprite.setAccelerationY(0);
            sprite.body.velocity.y = 0;
        }

        // Update sprite based on state
        if(sprite.body.velocity.x !== 0) {
            // left & right use same anim, it gets flipped
            sprite.anims.play("player-run", true);
        } else if(sprite.body.velocity.y !== 0){
            if(sprite.body.velocity.y < 0){
                // moving down
            } else if (sprite.body.velocity > 0){
                // moving up
            }
        } else{
            sprite.anims.stop();
            sprite.setTexture("player", 1);
        }
    }

    destroy(){
        this.sprite.destroy();
    }
    
}