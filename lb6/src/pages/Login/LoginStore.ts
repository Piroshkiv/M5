import {
    action, makeAutoObservable,
    makeObservable,
    observable,
    runInAction,
} from "mobx";
import {IUser} from "../../interfaces/users";
import * as userApi from "../../api/modules/users";
import AuthStore from "../../stores/AuthStore";
import { log } from "console";


class LoginStore {

    private authStore: AuthStore;

    email = '';
    password = '';
    error = '';
    isLoading = false;

    constructor(authStore: AuthStore) {
        this.authStore = authStore;
        makeAutoObservable(this);
    }

    changeEmail(email: string) {
        this.email = email;
        if (!!this.error) {
            this.error = '';
        }
       
    }

    changePassword(password: string) {
        this.password = password;
        if (!!this.error) {
            this.error = '';
        }
    }

    async login() {
        try {
            this.isLoading = true;
           await this.authStore.login(this.email, this.password);
           
        }
        catch (e) {
            if (e instanceof Error) {
                this.error = e.message;
                console.log(this);
                
            }
        }
        this.isLoading = false;
    }
}

export default LoginStore;