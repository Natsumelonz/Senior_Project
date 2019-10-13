import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueRouter from 'vue-router';

Vue.use(VueRouter)
Vue.config.productionTip = false

/*Path*/
//import path eg. import User from './Users.vue';
import Home from './components/HelloWorld';
import Menubar from './components/menubar';
import Alphabet from './Pages/alphabet';

const routes = [
  //path pattern eg.{ path: '/users' , component: User }
  { path: '/' , component: Home },
  { path: '/menu' , component: Menubar },
  { path: '/alphabet' , component: Alphabet }
]

const router = new VueRouter({
  routes,
  mode: 'history'
});

new Vue({
  vuetify,
  router,
  render: h => h(App)
}).$mount('#app')
