import Vue from 'vue'
import VueRouter from 'vue-router'
import { routes } from './routes'

Vue.use(VueRouter)

let router = new VueRouter({
  mode: 'history',
  routes
})

router.beforeEach((to, from, next) => {
  const isPublic = to.matched.some(record => record.meta.public)
  const onlyWhenLoggedout = to.matched.some(
    record => record.meta.onlyWhenLoggedout
  )

  console.log('Before Route', Vue.$store)

  next()
})

export default router
