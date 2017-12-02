import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import { component as VueFormGenerator, validators } from 'vue-form-generator';
import axios from 'axios'

@Component({
    components: {
        'vue-form-generator': VueFormGenerator
    }
})
export default class AssetFormComponent extends Vue {
    @Prop()
    initialModel: object;

    internalModel: any;

    initialCountryId: number;
    initialMimeTypeId: number;

    @Prop({default: "create"})
    mode: string;

    countriesList: Array<any>;
    mimeTypesList: Array<any>;

    formOptions: {};

    constructor() {
        super();

        this.formOptions = {};
        this.countriesList = [];
        this.mimeTypesList = [];
        this.internalModel = {};

        this.getListData('/api/MimeTypes', (self, data) => {
            self.mimeTypesList = data;

            //hack to fix binding emit of undefined for property when loading
            if (self.internalModel != undefined) {
                self.internalModel.mimeTypeId = self.initialMimeTypeId;
            }
        });

        this.getListData('/api/Countries', (self, data) => {
            self.countriesList = data;

            //hack to fix binding emit of undefined for property when loading
            if (self.internalModel != undefined) {
                self.internalModel.countryId = self.initialCountryId;
            }
        });

    }

    @Watch('initialModel', { immediate: false, deep: false })
    onInitialModelChanged(val: object, oldVal: object) { 
        console.log('initialModel changed: ' + JSON.stringify(val));
        this.internalModel = val;
        this.initialCountryId = this.internalModel.countryId;
        this.initialMimeTypeId = this.internalModel.mimeTypeId;;
    }

    get model() {
        return this.internalModel;
    }

    set model(value) {
        this.internalModel = value;
    }

    get schema() {
        return {
            fields: this.fields
        }
    }

    get countries() {
        return this.countriesList;
    }

    get mimeTypes() {
        return this.mimeTypesList;
    }

    get fields() {
        return [
            {
                type: 'input',
                inputType: 'text',
                label: 'ID',
                model: 'assetId',
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
                values: this.countries,
                required: true,
                validator: validators.required
            }, {
                type: 'select',
                label: 'Mime Type',
                model: 'mimeTypeId',
                values: this.mimeTypes,
                required: true,
                validator: validators.required
            }, {
                type: 'input',
                inputType: 'hidden',
                visible: false,
                model: 'assetKey'
            }, {
                type: 'input',
                inputType: 'hidden',
                visible: false,
                model: 'version'
            }
        ];
    }

    getListData(endpoint: string, callback: (self: AssetFormComponent, data: any) => void) {
        try {
            var self = this;
            //TODO: Cache these
            axios.get(endpoint)
               .then(response => {
                   //console.log(response.data);
                   console.log("retrieved data: " + response.data.length)
                   callback(self, response.data);

               })
               .catch(e => console.log(e));
        }
        catch (error) {
            console.log(error);
        }
    }
}