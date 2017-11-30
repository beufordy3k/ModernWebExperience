import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Vuetable, VuetablePagination } from 'vuetable-2';

Vue.use(Vuetable);

interface IPaginationData {
    total: number;
    per_page: number;
    current_page: number;
    last_page: number;
    next_page_url: string;
    prev_page_url: string;
    from: number;
    to: number;
}

@Component
({
    components: {
        Vuetable,
        'vuetable-pagination': VuetablePagination
    }
})
export default class AssetsComponent extends Vue {
    $refs: any;
    fields: Array<any>;

    constructor() {
        super();
        this.fields = [
            {
                name: 'fileName',
                title: 'File Name'
            },
            {
                name: 'createdBy',
                title: 'Created By'
            },
            {
                name: 'email',
                title: 'Email'
            },
            {
                name: 'description',
                title: 'Description'
            },
            {
                name: 'country.name',
                title: 'Country'
            },
            {
                name: 'mimeType.name',
                title: 'Mime Type'
            },
            '__slot:actions'
        ];
    }

    onPaginationData(paginationData : IPaginationData) {
        this.$refs.pagination.setPaginationData(paginationData);
    }

    onChangePage(page : any) {
        this.$refs.vuetable.changePage(page);
    }

    mounted() {
        //this.pagination = this.$refs.pagination;
        //console.log('pagination object: ' + this.$refs.pagination);
        //console.log('pagination object2: ' + Vuetable.$refs.pagination);
    }
}
