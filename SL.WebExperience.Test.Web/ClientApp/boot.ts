import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';

Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./components/home/home.vue.html') },
    {
        path: '/assetmanagement',
        component: require('./components/assetmanagement/assethome.vue.html'),
        children: [
            { path: '', component: require('./components/assetmanagement/assets.vue.html') },
            { path: 'new', component: require('./components/assetmanagement/newasset.vue.html'), props: true },
            { path: ':id/edit', name: 'edit-asset', component: require('./components/assetmanagement/editasset.vue.html'), props: true }
        ]
    }
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});
