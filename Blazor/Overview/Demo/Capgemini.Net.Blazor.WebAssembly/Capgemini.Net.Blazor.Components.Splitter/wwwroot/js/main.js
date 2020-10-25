var Capgemini;
(function (Capgemini) {
    var Net;
    (function (Net) {
        var Blazor;
        (function (Blazor) {
            var Components;
            (function (Components) {
                var Splitter;
                (function (Splitter_1) {
                    var Splitter = /** @class */ (function () {
                        function Splitter() {
                        }
                        Splitter.prototype.getBoundingClientRect = function (element) {
                            console.log(element.getBoundingClientRect().toJSON());
                            return element.getBoundingClientRect();
                        };
                        return Splitter;
                    }());
                    function load() {
                        window['capgemini_net_blazor_components_splitter'] = new Splitter();
                    }
                    Splitter_1.load = load;
                })(Splitter = Components.Splitter || (Components.Splitter = {}));
            })(Components = Blazor.Components || (Blazor.Components = {}));
        })(Blazor = Net.Blazor || (Net.Blazor = {}));
    })(Net = Capgemini.Net || (Capgemini.Net = {}));
})(Capgemini || (Capgemini = {}));
Capgemini.Net.Blazor.Components.Splitter.load();
//# sourceMappingURL=main.js.map