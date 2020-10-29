var Capgemini;
(function (Capgemini) {
    var Net;
    (function (Net) {
        var Blazor;
        (function (Blazor) {
            var Components;
            (function (Components) {
                var Demo1 = /** @class */ (function () {
                    function Demo1() {
                        this.rate = 0;
                        this.tempRate = 0;
                        this.updateRateIconActiveState();
                    }
                    Demo1.prototype.updateRateIconActiveState = function () {
                        var rateIcons = document.getElementsByClassName('rate-icon');
                        for (var i = 0; i < rateIcons.length; i += 1) {
                            var rateIcon = rateIcons.item(i);
                            if (this.isActive(i)) {
                                rateIcon.classList.add(Demo1.ACTIVE_STYLE);
                                rateIcon.classList.remove(Demo1.INACTIVE_STYLE);
                            }
                            else {
                                rateIcon.classList.remove(Demo1.ACTIVE_STYLE);
                                rateIcon.classList.add(Demo1.INACTIVE_STYLE);
                            }
                        }
                    };
                    Demo1.prototype.setRate = function () {
                        this.rate = this.tempRate;
                        this.updateRateIconActiveState();
                    };
                    Demo1.prototype.showRate = function (index) {
                        this.tempRate = index;
                        this.updateRateIconActiveState();
                    };
                    Demo1.prototype.revertRate = function () {
                        this.tempRate = this.rate;
                        this.updateRateIconActiveState();
                    };
                    Demo1.prototype.isActive = function (index) {
                        return index <= this.tempRate;
                    };
                    Demo1.ACTIVE_STYLE = "fas";
                    Demo1.INACTIVE_STYLE = "far";
                    return Demo1;
                }());
                function load() {
                    window['CapgeminiNetBlazorComponents'] = new Demo1();
                }
                Components.load = load;
            })(Components = Blazor.Components || (Blazor.Components = {}));
        })(Blazor = Net.Blazor || (Net.Blazor = {}));
    })(Net = Capgemini.Net || (Capgemini.Net = {}));
})(Capgemini || (Capgemini = {}));
Capgemini.Net.Blazor.Components.load();
//# sourceMappingURL=main.js.map