"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Player = void 0;
var vec3_1 = require("vec3");
var Player = /** @class */ (function () {
    function Player(id, position, velocity) {
        if (position === void 0) { position = new vec3_1.Vec3(0, 0, 0); }
        if (velocity === void 0) { velocity = new vec3_1.Vec3(0, 0, 0); }
        this.Id = id;
        this.Position = position;
        this.Velocity = velocity;
    }
    return Player;
}());
exports.Player = Player;
