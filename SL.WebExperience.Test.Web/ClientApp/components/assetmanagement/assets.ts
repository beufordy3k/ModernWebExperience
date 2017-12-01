import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Vuetable, VuetablePagination, VuetablePaginationInfo } from 'vuetable-2';
import VueRouter from 'vue-router';
import VueEvents from 'vue-events';

Vue.use(Vuetable);
Vue.use(VueEvents);

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
        'vuetable-pagination': VuetablePagination,
        VuetablePaginationInfo,
        VueRouter,
        'filter-bar': require('../shared/filterbar.vue.html')
    }
})
export default class AssetsComponent extends Vue {
    $refs: any;
    fields: Array<any>;
    queryParams: object;
    moreParams: object;
    sortOrder: Array<any>;

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

        this.queryParams = { sort: 'sort', page: 'pageNumber', perPage: 'pageSize' };

        this.moreParams = {};
        this.sortOrder = [
            {
                field: 'fileName',
                sortField: 'default',
                direction: 'asc'
            }
        ];
    }

    onPaginationData(paginationData : IPaginationData) {
        this.$refs.pagination.setPaginationData(paginationData);
        this.$refs.paginationInfo.setPaginationData(paginationData);
    }

    onChangePage(page : any) {
        this.$refs.vuetable.changePage(page);
    }

    editRow(rowData : any) {
        alert('You clicked edit on' + JSON.stringify(rowData));
        this.$router.push('/assetmanagement/edit'); //pass data
    }
    
    deleteRow(rowData : any) {
        alert('You clicked delete on' + JSON.stringify(rowData));
        //delete record
        //reload data
    }

    onFilterSet(filterText : string) {
        console.log('filter-set', filterText);
        this.moreParams = {
            'country': filterText
        }

        this.$nextTick(() => {

            this.$refs.vuetable.refresh();
        });
    }

    onFilterReset() {
        console.log('filter-reset');

        this.moreParams = {};

        this.$nextTick(() => this.$refs.vuetable.refresh());
    }

    mounted() {
        this.$events.$on('filter-set', eventData => this.onFilterSet(eventData));
        this.$events.$on('filter-reset', e => this.onFilterReset());
    }
}
