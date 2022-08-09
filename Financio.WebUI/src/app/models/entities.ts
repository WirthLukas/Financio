export class Account {
    public debitSideAccountings: AccountReference[] = [];
    public creditSideAccountings: AccountReference[] = [];

    constructor(
        public number: string,
        public name: string,
        public description = ''
    ) {}

    public getSumOfDebit(): number {
        return this.debitSideAccountings.length === 0
            ? 0
            : this.debitSideAccountings
                  .map((a) => a.value)
                  .reduce((a, b) => a + b);
    }

    public getSumOfCredit(): number {
        return this.creditSideAccountings.length === 0
            ? 0
            : this.creditSideAccountings
                  .map((a) => a.value)
                  .reduce((a, b) => a + b);
    }

    public getSaldo(): string {
        const debitSum = this.getSumOfDebit();
        const creditSum = this.getSumOfCredit();
        return `${Math.abs(debitSum - creditSum)} ${
            debitSum > creditSum ? 'S' : 'H'
        }`;
    }

    public duplicate(): Account {
        const newAccount = new Account(
            this.number,
            this.name,
            this.description
        );
        newAccount.debitSideAccountings = [...this.debitSideAccountings];
        newAccount.creditSideAccountings = [...this.creditSideAccountings];
        return newAccount;
    }

    public copy(account: Account) {
        this.number = account.number;
        this.name = account.name;
        this.description = account.description;
        this.debitSideAccountings = account.debitSideAccountings;
        this.creditSideAccountings = account.creditSideAccountings;
    }
}

export class AccountReference {
    constructor(
        public date: Date,
        public accounts: Account[],
        public value: number,
        public side: AccountSide
    ) {}

    public getAccountListAsString(): string {
        return this.accounts
            .map((acc) => `${acc.number} ${acc.name}`)
            .reduce((prv, cur) => `${prv}, ${cur}`);
    }
}

export enum AccountSide {
    Debit = 'Debit',
    Credit = 'Credit',
}

export class FormularEntry {
    public debitAccounts: FormularEntryBooking[] = [];
    public creditAccounts: FormularEntryBooking[] = [];

    public getDebitAsString(): string[] {
        return this.debitAccounts.map(
            (b) => `${b.account.number} ${b.account.name} with ${b.value}`
        );
        // .reduce((a, b) => `${a},\n${b}`);
    }

    public getCreditAsString(): string[] {
        return this.creditAccounts.map(
            (b) => `${b.account.number} ${b.account.name} with ${b.value}`
        );
        //         .reduce((a, b) => `${a},\n${b}`);
    }

    constructor(public date: Date, public notes = '') {}
}

export interface FormularEntryBooking {
    account: Account;
    value: number;
    side: AccountSide;
}
