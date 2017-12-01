import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import VueFormGenerator from 'vue-form-generator';

Vue.use(VueFormGenerator);

@Component({
    components: {
        'vue-form-generator': VueFormGenerator.component
    }
})
export default class NewAssetComponent extends Vue {
    model: {};
    schema: {};
    formOptions: {};

    countries: Array<any>;
    mimeTypes: Array<any>;

    constructor() {
        super();

        this.model = {};
        this.formOptions = {};

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
                    validator: VueFormGenerator.validators.email
                }, {
                    type: 'textArea',
                    label: 'Description',
                    model: 'description',
                    placeholder: 'Description of the file',
                    max: 500,
                    rows: 4,
                    required: false,
                    validator: VueFormGenerator.validators.string
                }
            ]
        };

        /*
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

    created() {
        this.getCountries();
    }

    getCountries() {
        try {
            //let response = await this.$http.get('/api/Countries'); 

            //TODO: Cache these
            //axios.get('/api/Countries')
            //    .then(response => {
            //        console.log(response.data);
            //        this.countries = response.data;
            //    })
            //    .catch((e) => console.log(e));
        }
        catch (error) {
            console.log(error);
        }
    }
}