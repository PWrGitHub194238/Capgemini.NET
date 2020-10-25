var Capgemini;
(function (Capgemini) {
    var Net;
    (function (Net) {
        var Blazor;
        (function (Blazor) {
            var Rate = /** @class */ (function () {
                function Rate() {
                    this.rate = 0;
                    this.tempRate = 0;
                    this.updateRateIconActiveState();
                }
                Rate.prototype.updateRateIconActiveState = function () {
                    var rateIcons = document.getElementsByClassName('rate-icon');
                    for (var i = 0; i < rateIcons.length; i += 1) {
                        var rateIcon = rateIcons.item(i);
                        if (this.isActive(i)) {
                            rateIcon.classList.add(Rate.ACTIVE_STYLE);
                            rateIcon.classList.remove(Rate.INACTIVE_STYLE);
                        }
                        else {
                            rateIcon.classList.remove(Rate.ACTIVE_STYLE);
                            rateIcon.classList.add(Rate.INACTIVE_STYLE);
                        }
                    }
                };
                Rate.prototype.setRate = function () {
                    this.rate = this.tempRate;
                    this.updateRateIconActiveState();
                };
                Rate.prototype.showRate = function (index) {
                    this.tempRate = index;
                    this.updateRateIconActiveState();
                };
                Rate.prototype.revertRate = function () {
                    this.tempRate = this.rate;
                    this.updateRateIconActiveState();
                };
                Rate.prototype.isActive = function (index) {
                    return index <= this.tempRate;
                };
                Rate.ACTIVE_STYLE = "fas";
                Rate.INACTIVE_STYLE = "far";
                return Rate;
            }());
            function load() {
                window['rate'] = new Rate();
            }
            Blazor.load = load;
        })(Blazor = Net.Blazor || (Net.Blazor = {}));
    })(Net = Capgemini.Net || (Capgemini.Net = {}));
})(Capgemini || (Capgemini = {}));
Capgemini.Net.Blazor.load();
//# sourceMappingURL=main.js.map