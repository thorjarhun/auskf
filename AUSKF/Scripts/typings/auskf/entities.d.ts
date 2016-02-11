declare module System {
    interface Guid {
    }
    interface Tuple<T1, T2> {
        item1: T1;
        item2: T2;
    }
}

declare module AUSKF.Domain.Entities.Identity {
    interface User extends Microsoft.AspNet.Identity.EntityFramework.IdentityUser<number,
        AUSKF.Domain.Entities.Identity.UserLogin,
        AUSKF.Domain.Entities.Identity.UserRole,
        AUSKF.Domain.Entities.Identity.UserClaim> {
        userName: string;
        displayName: string;
        password: string;
        passwordLastChangedDate: Date;
        maximumDaysBetweenPasswordChange: number;
        postCount: number;
        topicCount: number;
        email: string;
        tagline: string;
        location: string;
        lastSearch: string;
        rankId: number;
        joinedDate: Date;
        lastLogin: Date;
        karma: number;
        avatarId: number;
        active: boolean;
        rowVersion: number[];
        notes: string;
        userProfileId: number;
        profile: AUSKF.Domain.Entities.Identity.UserProfile;
    }
    interface UserRole extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<number> {
    }
    interface UserClaim extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<number> {
        userClaimId: number;
        user: AUSKF.Domain.Entities.Identity.User;
    }
    interface UserLogin extends Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<number> {
        userLoginId: number;
        user: AUSKF.Domain.Entities.Identity.User;
    }
    interface UserProfile {
        userProfileId: number;
        birthDay: Date;
        location: string;
        latitude: number;
        longitude: number;
        sig: string;
        allowHtmlSig: boolean;
        facebookPage: string;
        skypeUserName: string;
        twitterName: string;
        homePage: string;
    }
}

declare module Microsoft.AspNet.Identity.EntityFramework {
    interface IdentityUser<TKey, TLogin, TRole, TClaim> {
        email: string;
        emailConfirmed: boolean;
        passwordHash: string;
        securityStamp: string;
        phoneNumber: string;
        phoneNumberConfirmed: boolean;
        twoFactorEnabled: boolean;
        lockoutEndDateUtc: Date;
        lockoutEnabled: boolean;
        accessFailedCount: number;
        roles: TRole[];
        claims: TClaim[];
        logins: TLogin[];
        id: TKey;
        userName: string;
    }
    interface IdentityUserRole<TKey> {
        userId: TKey;
        roleId: TKey;
    }
    interface IdentityUserClaim<TKey> {
        id: number;
        userId: TKey;
        claimType: string;
        claimValue: string;
    }
    interface IdentityUserLogin<TKey> {
        loginProvider: string;
        providerKey: string;
        userId: TKey;
    }
}

declare module AUSKF.Domain.Entities {
    import User = Domain.Entities.Identity.User;

    interface EntityBase {
        createDate: Date;
        modifyDate: Date;
    }

    interface Dojo extends EntityBase {
        dojoId: System.Guid;
        federationId: System.Guid;
        addressId: System.Guid;
        primaryContactId: System.Guid;
        dojoName: string;
        phone: string;
        websiteUrl: string;
        emailAddress: string;
        notes: string;
    }

    interface Federation extends EntityBase {
        federationId: System.Guid;
        name: string;
        email: string;
        phone: string;
        websiteUrl: string;
    }  

    interface Event extends EntityBase {
        eventId: System.Guid;
        eventDate: Date;
        title: string;
        eventText: string;
        description: string;
        createdBy: User;
        id: System.Guid;
    }
}
declare module AUSKF.Domain.Collections{
    interface SerializablePagination<T> extends BasePagination {
        startingIndex: number;
        pageArray: string[];
        pageArraySize: number;
        pageNumber: number;
        currentPage: T[];
        pageSize: number;
        totalItems: number;
        totalPages: number;
        hasPreviousPage: boolean;
        hasNextPage: boolean;
        baseUrl: string;
    }
    interface BasePagination {
        totalPages: number;
        pageArraySize: number;
        pageNumber: number;
    }
}
declare module AUSKF.Domain.Interfaces {
    enum SortDirection {
        Ascending,
        Descending
    }
}

declare module AUSKF.Domain.Models.Account {

    interface UserInfoViewModel {
        email: string;
        hasRegistered: boolean;
        loginProvider: string;
    }

    interface SearchValues {
        page: number;
        pageSize: number;
        sortDirection: string;
        orderBy: string;
        query: string;
        onlyShowActive: boolean;
    }
}