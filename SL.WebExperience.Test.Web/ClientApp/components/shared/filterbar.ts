import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import VueEvents from 'vue-events'

Vue.use(VueEvents);

@Component
export default class FilterBarComponent extends Vue {

    filterText: string;

    constructor() {
        super();

        this.filterText = '';
    }

    doFilter() {
        console.log('doFilter:', this.filterText);
        this.$events.fire('filter-set', this.filterText);
    }

    resetFilter() {
        this.filterText = '';
        console.log('resetFilter');
        this.$events.fire('filter-reset');
    }
}