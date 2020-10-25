
namespace Capgemini.Net.Blazor {

    class Rate {
        private static ACTIVE_STYLE: string = "fas";
        private static INACTIVE_STYLE: string = "far";

        public rate: number = 0;
        public tempRate: number = 0;

        constructor() {
            this.updateRateIconActiveState();
        }

        public updateRateIconActiveState() {
            const rateIcons = document.getElementsByClassName('rate-icon');

            for (let i: number = 0; i < rateIcons.length; i += 1) {
                const rateIcon = rateIcons.item(i);

                if (this.isActive(i)) {
                    rateIcon.classList.add(Rate.ACTIVE_STYLE);
                    rateIcon.classList.remove(Rate.INACTIVE_STYLE);
                } else {
                    rateIcon.classList.remove(Rate.ACTIVE_STYLE);
                    rateIcon.classList.add(Rate.INACTIVE_STYLE);
                }
            }
        }

        public setRate() {
            this.rate = this.tempRate;
            this.updateRateIconActiveState();
        }

        public showRate(index: number) {
            this.tempRate = index;
            this.updateRateIconActiveState();
        }

        public revertRate() {
            this.tempRate = this.rate;
            this.updateRateIconActiveState();
        }

        private isActive(index: number): boolean {
            return index <= this.tempRate;
        }
    }

    export function load() {
        window['rate'] = new Rate();
    }
}

Capgemini.Net.Blazor.load();