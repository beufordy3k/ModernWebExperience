import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface IAsset {
    createdWhen: string;
    updatedWhen: string;
    version: number;
    fileName: string;
    createdBy: string;
    email: string;
    description: string;
    country: object;
    mimeType: object;
}

@Component
export default class FetchDataComponent extends Vue {
    assets: IAsset[] = [];

    mounted() {
        fetch('/api/Assets')
            .then(response => response.json() as Promise<IAsset[]>)
            .then(data => {
                this.assets = data;
            });
    }
}
