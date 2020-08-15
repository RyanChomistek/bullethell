"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Lobby = void 0;
var Lobby = /** @class */ (function () {
    function Lobby() {
        var _this = this;
        this.GetNextPlayerId = function () {
            var id = _this.playerIdCnt;
            _this.playerIdCnt++;
            return id;
        };
        this.AddPlayer = function (player) {
            _this.PlayerMap.set(player.Id, player);
        };
        this.PlayerMap = new Map();
        this.playerIdCnt = 0;
    }
    return Lobby;
}());
exports.Lobby = Lobby;
