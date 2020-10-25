
namespace Capgemini.Net.Blazor.Components.Splitter {

    class Splitter {
        public getBoundingClientRect(element: HTMLElement): DOMRect {
            console.log(element.getBoundingClientRect().toJSON());
            return element.getBoundingClientRect();
        }
    }

    export function load() {
        window['capgemini_net_blazor_components_splitter'] = new Splitter();
    }
}

Capgemini.Net.Blazor.Components.Splitter.load();