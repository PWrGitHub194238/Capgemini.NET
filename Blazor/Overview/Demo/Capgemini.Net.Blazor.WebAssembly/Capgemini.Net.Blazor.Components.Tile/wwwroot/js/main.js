var Capgemini;
(function (Capgemini) {
    var Net;
    (function (Net) {
        var Blazor;
        (function (Blazor) {
            var Components;
            (function (Components) {
                var Tile;
                (function (Tile_1) {
                    var Tile = /** @class */ (function () {
                        function Tile() {
                        }
                        Tile.prototype.setSplitterPosition = function (keyName, splitterLeftPosition, splitterRightPosition) {
                            this.setItem(keyName, JSON.stringify([splitterLeftPosition, splitterRightPosition]));
                        };
                        Tile.prototype.getSplitterPosition = function (keyName) {
                            var value = this.getItem(keyName);
                            return value ? JSON.parse(value) : [50, 50];
                        };
                        Tile.prototype.getItem = function (keyName) {
                            return localStorage.getItem(keyName);
                        };
                        Tile.prototype.setItem = function (keyName, keyValue) {
                            localStorage.setItem(keyName, keyValue);
                        };
                        return Tile;
                    }());
                    function load() {
                        window['capgemini_net_blazor_components_tile'] = new Tile();
                    }
                    Tile_1.load = load;
                })(Tile = Components.Tile || (Components.Tile = {}));
            })(Components = Blazor.Components || (Blazor.Components = {}));
        })(Blazor = Net.Blazor || (Net.Blazor = {}));
    })(Net = Capgemini.Net || (Capgemini.Net = {}));
})(Capgemini || (Capgemini = {}));
Capgemini.Net.Blazor.Components.Tile.load();
//# sourceMappingURL=main.js.map