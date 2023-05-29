import { makeAutoObservable} from "mobx";
import * as authApi from "../api/modules/auth";

class AuthStore {
    token = "";
    error = "";

    constructor() {
        makeAutoObservable(this);
    }

    async login(email: string, password: string) {
        const result = await authApi.login({email, password});

        console.log(email + password);

        if(result.status && result.status == 400)
        {
            this.error = result.message;
        }
        this.token = result.token;


    }
    async register(email: string, password: string) {

        console.log(email + password);
        const result = await authApi.register({email, password});
        console.log(result);
        if(result.status && result.status == 400)
        {
            this.error = result.message;
        }
        this.token = result.token;
    }
    async logout(){
        const result = await authApi.logout();
        this.token = '';
    }
}

export default AuthStore;