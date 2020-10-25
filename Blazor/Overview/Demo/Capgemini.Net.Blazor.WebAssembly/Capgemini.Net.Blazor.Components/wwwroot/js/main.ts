namespace Capgemini.Net.Blazor.Components {
    class Components {
        getContextPointStates(context: DemoChecklistContext): { [key: string]: boolean } {
            const contextPoints = context.points;

            const result = {};
            contextPoints.forEach((contextPoint: DemoChecklistPointContext) => {
                const keyName = this.getContextPointKeyName(context, contextPoint);
                const value = this.getItem(keyName) === 'true';
                result[keyName] = value;
            });

            return result;
        }

        setContextPointState(keyName: string, isDone: boolean) {
            this.setItem(keyName, String(isDone));
        }

        private getContextPointKeyName(context: DemoChecklistContext, contextPoint: DemoChecklistPointContext): string {
            return `${context.name}_${contextPoint.name}`;
        }


        private getItem(keyName: string): string {
            return localStorage.getItem(keyName);
        }

        private setItem(keyName: string, keyValue: string) {
            localStorage.setItem(keyName, keyValue);
        }
    }

    interface DemoChecklistContext {
        name: string;
        points: DemoChecklistPointContext[];
    }

    interface DemoChecklistPointContext {
        name: string;
        isDone: boolean;
    }

    export function load() {
        window['capgemini_net_blazor_components'] = new Components();
    }
}

Capgemini.Net.Blazor.Components.load();