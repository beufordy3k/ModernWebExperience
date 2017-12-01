import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { component as VueFormGenerator, validators } from 'vue-form-generator';
import axios from 'axios'

interface ICountryData {
    id: number;
    name: string;
}

@Component({
    components: {
        'vue-form-generator': VueFormGenerator
    }
})
export default class NewAssetComponent extends Vue {
    model: {};
    schema: any;
    formOptions: {};

    countries: Array<any>;
    mimeTypes: Array<any>;

    constructor() {
        super();

        this.model = {};
        this.formOptions = {};

        this.countries = [];
        
        this.schema = {
            fields: [
                {
                    type: 'input',
                    inputType: 'text',
                    label: 'ID (disabled text field)',
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
                    required: true
                }, {
                    type: 'input',
                    inputType: 'text',
                    label: 'Created By',
                    model: 'createdBy',
                    placeholder: 'Creator of the file',
                    required: true
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
                    validator: validators.required
                }
            ]
        };

        this.getCountries((data) => {
            this.schema.fields[5].values = data; //TODO: Fix this, it's a bad way to go.
        });

        /*
        this.schema.fields[5].values = this.countries;
       , {
           type: 'input',
           inputType: 'password',
           label: 'Password',
           model: 'password',
           min: 6,
           required: true,
           hint: 'Minimum 6 characters',
           validator: VueFormGenerator.validators.string
       }, {
           type: 'select',
           label: 'Skills',
           model: 'skills',
           values: ['Javascript', 'VueJS', 'CSS3', 'HTML5']
       }, {
           type: 'input',
           inputType: 'email',
           label: 'E-mail',
           model: 'email',
           placeholder: "User's e-mail address"
       }, {
           type: 'checkbox',
           label: 'Status',
           model: 'status',
           default: true
       }
       */
    }

    beforeMount() {
        console.log('newasset before mount');
    }        
        
    created() {
        console.log('newasset created');
    }

    mounted() {
        console.log('newasset mount');
    }

    getCountries(callback: (data: any) => void) {
        try {
            //TODO: Cache these
            axios.get('/api/Countries')
               .then(response => {
                   //console.log(response.data);
                   console.log("retrieved countries data: " + response.data.length)
                   callback(response.data);

               })
               .catch((e) => console.log(e));
        }
        catch (error) {
            console.log(error);
        }
    }
}