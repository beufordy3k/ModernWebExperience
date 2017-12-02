import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Vuetable, VuetablePagination, VuetablePaginationInfo } from 'vuetable-2';
import VueRouter from 'vue-router';
import VueEvents from 'vue-events';
import axios from 'axios'

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
        console.log(`data: ${JSON.stringify(rowData)}`);
        this.$router.push({name: 'edit-asset', params: {id: rowData.assetId}}); //pass data
    }
    
    deleteRow(rowData : any) {
        let result = confirm(`Are you sure you want to delete the ${rowData.fileName} record?`);
        console.log(`Delete Record: ${result}`);

        if (!result) {
            return;
        }

        let id = rowData.assetId;

        //delete record
        axios.delete(`/api/Assets/${id}`)
        .then(response => {
            console.log(`Record ${id} has been deleted!`);
            //reload data
            this.$refs.vuetable.reload();
        })
        .catch(e => {
            console.log(e);
        });
        
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
