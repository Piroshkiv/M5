import { makeAutoObservable} from "mobx";
import * as usersApi from "../api/modules/users";
import {IUser} from "../interfaces/users";

class CreateStore {
    id = "";

    constructor() {
        makeAutoObservable(this);
    }

    async add(name: string, job: string) {
        const result = await usersApi.addUser({name, job});
        this.id = result.id;
        console.log(result);
    }
}

export default CreateStore;