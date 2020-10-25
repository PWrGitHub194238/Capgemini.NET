namespace Capgemini.Net.Blazor.Components.Tile {
    class Tile {
        public setSplitterPosition(keyName: string, splitterLeftPosition: number, splitterRightPosition: number) {
            this.setItem(keyName, JSON.stringify([splitterLeftPosition, splitterRightPosition ]));
        }

        public getSplitterPosition(keyName: string): number[] {
            const value = this.getItem(keyName);
            return value ? JSON.parse(value) : [50, 50];
        }

        private getItem(keyName: string): string {
            return localStorage.getItem(keyName);
        }

        private setItem(keyName: string, keyValue: string) {
            localStorage.setItem(keyName, keyValue);
        }
    }

    export function load() {
        window['capgemini_net_blazor_components_tile'] = new Tile();
    }
}

Capgemini.Net.Blazor.Components.Tile.load();