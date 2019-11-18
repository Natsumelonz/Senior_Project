import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueRouter from 'vue-router';
import VueAxios from 'vue-axios'
import axios from 'axios'

Vue.use(VueRouter,axios,VueAxios)
Vue.config.productionTip = false

/*Path*/
//import path eg. import User from './Users.vue';
import Alphabet from './Pages/alphabet';
import Word from './Pages/word'
import Dialog from './Pages/dialog'

const routes = [
  //path pattern eg.{ path: '/users' , component: User }
  { path: '/' , redirect: '/dialog' },
  { path: '/alphabet' , component: Alphabet },
  { path: '/word' , component: Word},
  { path: '/dialog' , component: Dialog}
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
