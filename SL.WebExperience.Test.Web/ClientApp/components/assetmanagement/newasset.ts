import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { component as VueFormGenerator, validators } from 'vue-form-generator';
import axios from 'axios'
import { uuid } from 'vue-uuid'

@Component({
    components: {
        'vue-form-generator': VueFormGenerator
    }
})
export default class NewAssetComponent extends Vue {
    model: any;
    schema: any;
    formOptions: {};

    constructor() {
        super();

        this.model = {};
        this.formOptions = {};

        this.schema = {
            fields: [
                {
                    type: 'input',
                    inputType: 'text',
                    label: 'ID',
                    model: 'id',
                    readonly: true,
                    disabled: true
                }, {
                    type: 'input',
                    inputType: 'text',
                    label: 'File Name',
                    model: 'fileName',
                    placeholder: 'Name of the file',
                    featured: true,
                    required: true,
                    validator: validators.string
                }, {
                    type: 'input',
                    inputType: 'text',
                    label: 'Created By',
                    model: 'createdBy',
                    placeholder: 'Creator of the file',
                    required: true,
                    validator: validators.string
                }, {
                    type: 'input',
                    inputType: 'text',
                    label: 'Email',
                    model: 'email',
                    placeholder: 'Email Address',
                    required: true,
                    validator: validators.email
                }, {
                    type: 'textArea',
                    label: 'Description',
                    model: 'description',
                    placeholder: 'Description of the file',
                    max: 500,
                    rows: 4,
                    required: false,
                    validator: validators.string
                }, {
                    type: 'select',
                    label: 'Country',
                    model: 'countryId',
                    values: [],
                    required: true,
                }, {
                    type: 'select',
                    label: 'Mime Type',
                    model: 'mimeTypeId',
                    values: [],
                    required: true,
                }
            ]
        };

        this.getListData('/api/Countries', (data) => {
            this.schema.fields[5].values = data; //TODO: Fix this, it's a bad way to go.
        });

        this.getListData('/api/MimeTypes', (data) => {
            this.schema.fields[6].values = data; //TODO: Fix this, it's a bad way to go.
        });
    }

    getListData(endpoint: string, callback: (data: any) => void) {
        try {
            //TODO: Cache these
            axios.get(endpoint)
               .then(response => {
                   //console.log(response.data);
                   console.log("retrieved data: " + response.data.length)
                   callback(response.data);

               })
               .catch(e => console.log(e));
        }
        catch (error) {
            console.log(error);
        }
    }

    testAlert() {
        console.log('you got it.')
    }

    createNewAsset() {
        let data = this.model;

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