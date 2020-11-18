
namespace Capgemini.Net.Blazor.Components {
    class Demo01 {
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
                    rateIcon.classList.add(Demo01.ACTIVE_STYLE);
                    rateIcon.classList.remove(Demo01.INACTIVE_STYLE);
                } else {
                    rateIcon.classList.remove(Demo01.ACTIVE_STYLE);
                    rateIcon.classList.add(Demo01.INACTIVE_STYLE);
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
        window['CapgeminiNetBlazorComponents'] = new Demo01();
    }
}

Capgemini.Net.Blazor.Components.load();