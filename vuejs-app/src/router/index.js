/**
 * @file router/index.js
 * @description Vue Router configuration for the application.
 * Defines all application routes and implements authentication guards.
 */

import { createRouter, createWebHashHistory } from 'vue-router'
import store from '@/store';
import DataFetcher from '@/components/DataFetcher.vue';
import CreateItem from '@/views/CreateItem.vue';
import EditItem from '@/views/EditItem.vue';
import Login from '@/views/Login.vue';

/**
 * Application route definitions.
 * @type {Array<import('vue-router').RouteRecordRaw>}
 */
const routes = [
  {
    path: '/',
    name: 'home',
    component: DataFetcher,
    meta: { requiresAuth: true }
  },
  {
    path: '/create',
    name: 'CreateItem',
    component: CreateItem,
    meta: { requiresAuth: true }
  },
  {
    path: '/edit/:id',
    name: 'EditItem',
    component: EditItem,
    meta: { requiresAuth: true }
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  }
];

/**
 * Vue Router instance with hash history mode.
 * @type {import('vue-router').Router}
 */
const router = createRouter({
  history: createWebHashHistory(),
  routes
});

/**
 * Navigation guard that checks authentication before each route.
 * Redirects unauthenticated users to the login page for protected routes.
 */
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!store.getters.isAuthenticated) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      });
    } else {
      next();
    }
  } else {
    next();
  }
});

export default router
