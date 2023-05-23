// pages
import Resources from "./pages/Resources";
import Home from "./pages/Home";
import Resource from "./pages/Resource";
import User from "./pages/User";
import AddUser from "./pages/AddUser"


// other
import {FC} from "react";
import Login from "./pages/Login";
import Register from "./pages/Register/Register";

// interface
interface Route {
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<{}>
}

export const routes: Array<Route> = [
    {
        key: 'home-route',
        title: 'Home',
        path: '/',
        enabled: true,
        component: Home
    },
    {
        key: 'resources-route',
        title: 'Resources',
        path: '/resources',
        enabled: true,
        component: Resources
    },
    {
        key: 'resource-route',
        title: 'Resource',
        path: '/resource/:id',
        enabled: false,
        component: Resource
    },
    {
        key: 'user-route',
        title: 'User',
        path: '/user/:id',
        enabled: false,
        component: User
    },
    {
        key: 'add-user-route',
        title: 'AddUser',
        path: '/addUser',
        enabled: true,
        component: AddUser
    },
    {
        key: 'login-route',
        title: 'Login',
        path: '/login',
        enabled: false,
        component: Login
    },
    {
        key: 'register-route',
        title: 'Register',
        path: '/register',
        enabled: false,
        component: Register
    }
]