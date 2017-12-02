import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import axios from 'axios'

@Component({
    components: {
        'asset-form': require('../shared/assetform.vue.html')
    }
})
export default class EditAssetComponent extends Vue {
    @Prop({default: -1})
    id: number;

    modelData: object;

    constructor() {
        super();

        this.modelData = {};
    }

    mounted() {
        console.log(`data: ${this.id}`);

        this.getAsset(this.id);
    }

    get model() {
        return this.modelData;
    }

    getAsset(id: number) {
        if (id === -1) {
            return;
        }

        let self = this;
        axios.get(`/api/Assets/${id}`)
        .then(response => {
            self.modelData = response.data;

            console.log(`asset: ${JSON.stringify(this.model)}`);
        })
        .catch(e => console.log(e));
    }

    updateAsset() {
        //console.log('update data: ' + JSON.stringify(this.model));

        let data = this.model as any;

        axios.put(`/api/Assets/${data.assetId}`, data)
        .then(response => {
            console.log(`Response: ${response.status}:${response.statusText}`);
            this.$router.push('/assetmanagement');
        })
        .catch(e => console.log(e));

    }
}