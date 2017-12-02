import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import axios from 'axios'
import { uuid } from 'vue-uuid'

@Component({
    components: {
        'asset-form': require('../shared/assetform.vue.html')
    }
})
export default class NewAssetComponent extends Vue {
    modelData: object = {};

    constructor() {
        super();
    }

    get model() {
        return this.modelData;
    }

    set model(value) {
        this.modelData = value;
    }

    mounted() {
        this.modelData = {
            fileName: "",
            createdBy: "",
            email: "",
            description: "",
            countryId: -1,
            mimeTypeId: -1
        };
    }

    createNewAsset() {
        let data = this.model as any;

        data.version = '1';
        data.assetKey = uuid.v4();

        console.log('New asset creation: ' + JSON.stringify(data));
        axios.post('/api/Assets', data)
        .then(response => {
            console.log('Response: ' + response.status + ':' + response.statusText);
            this.$router.push('/assetmanagement');
        })
        .catch(e => {
            console.log(e);
        });
    }    
}