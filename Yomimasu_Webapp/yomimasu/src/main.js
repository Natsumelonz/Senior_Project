import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueRouter from 'vue-router';

Vue.use(VueRouter)
Vue.config.productionTip = false

/*Path*/
//import path eg. import User from './Users.vue';

const routes = [
  //path pattern eg.{ path: '/users' , component: User }
]

const router = new VueRouter({
  routes,
  mode: 'history'
});

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
