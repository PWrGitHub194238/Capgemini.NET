var Capgemini;
(function (Capgemini) {
    var Net;
    (function (Net) {
        var Blazor;
        (function (Blazor) {
            var Components;
            (function (Components_1) {
                var Components = /** @class */ (function () {
                    function Components() {
                    }
                    Components.prototype.getContextPointStates = function (context) {
                        var _this = this;
                        var contextPoints = context.points;
                        var result = {};
                        contextPoints.forEach(function (contextPoint) {
                            var keyName = _this.getContextPointKeyName(context, contextPoint);
                            var value = _this.getItem(keyName) === 'true';
                            result[keyName] = value;
                        });
                        return result;
                    };
                    Components.prototype.setContextPointState = function (keyName, isDone) {
                        this.setItem(keyName, String(isDone));
                    };
                    Components.prototype.getContextPointKeyName = function (context, contextPoint) {
                        return context.name + "_" + contextPoint.name;
                    };
                    Components.prototype.getItem = function (keyName) {
                        return localStorage.getItem(keyName);
                    };
                    Components.prototype.setItem = function (keyName, keyValue) {
                        localStorage.setItem(keyName, keyValue);
                    };
                    return Components;
                }());
                function load() {
                    window['capgemini_net_blazor_components'] = new Components();
                }
                Components_1.load = load;
            })(Components = Blazor.Components || (Blazor.Components = {}));
        })(Blazor = Net.Blazor || (Net.Blazor = {}));
    })(Net = Capgemini.Net || (Capgemini.Net = {}));
})(Capgemini || (Capgemini = {}));
Capgemini.Net.Blazor.Components.load();
//# sourceMappingURL=main.js.map