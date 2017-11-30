import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import VueFormGenerator from 'vue-form-generator';

@Component({
    components: {
        'vue-form-generator': VueFormGenerator.component
    }
})
export default class NewAssetComponent extends Vue {
    model: {};
    schema: {};
    formOptions: {};
}