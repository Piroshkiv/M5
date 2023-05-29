import {
    action, makeAutoObservable,
    makeObservable,
    observable,
    runInAction,
} from "mobx";
import {IUser} from "../../interfaces/users";
import * as userApi from "../../api/modules/users";
import CreateStore from "../../stores/CreateStore";


class AddUserStore {

    private createStore: CreateStore;

    name = '';
    job = '';
    error = '';
    id = '';
    isLoading = false;

    constructor(createStore: CreateStore) {
        this.createStore = createStore;
        makeAutoObservable(this);
    }

    changeName(name: string) {
        this.name = name;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeJob(job: string) {
        this.job = job;
        if (!!this.error) {
            this.error = '';
        }
    }

    async add() {
        try {
            this.isLoading = true;
           await this.createStore.add(this.name, this.job);
           this.id = this.createStore.id;
           console.log(this);
        }
        catch (e) {
            if (e instanceof Error) {
                this.error = e.message;
            }
        }
        this.isLoading = false;
    }
}

export default AddUserStore;