# DotNest SDK readme

## Overview

[DotNest SDK](https://github.com/Lombiq/DotNest-SDK) is a local developer environment for building Media Themes to be deployed to Orchard 1 sites running on [DotNest](https://dotnest.com).

Creating Orchard 1 sites is no longer available on DotNest (but existing ones are still running), you can only create Orchard Core ones. If you're starting a new project, check out [DotNest Core SDK](https://github.com/Lombiq/DotNest-Core-SDK) instead.

The base of the source code on the `master` branch is the same Orchard version that is running on DotNest as well as all the hotfixes and mods we've applied to it. On top of that, all the open-source modules and themes are added as Git submodules, which gives you the ability to develop your theme and run your site locally in an environment quite close to the live DotNest site.

This project (along with [DotNest](https://dotnest.com), the [Orchard CMS](https://orchardproject.net) SaaS) is developed by [Lombiq Technologies Ltd](https://lombiq.com). Commercial-grade support is available through Lombiq.

## Getting started

Fork the [DotNest SDK](https://github.com/Lombiq/DotNest-SDK) repository or create an empty repository and push the SDK's `dev` branch to it. For simplicity, we'll refer to your repository as `fork` from now on and assume a simple branching strategy with only one additional branch for development, but your use-case can be more complex.

## Working with the repository

- Whenever you create any branches, make sure to choose names that don't collide with the ones in the SDK. If your project is called e.g. `Awesome Project`, then your development branch should be created on top of `dev` and name it e.g. `dev-ap`.
- We recommend you create such a development branch and set it as the default branch of your fork.
- Create a custom solution file on your own development branch as a copy of `DotNest.SDK.sln`, e.g. `AwesomeProject.sln`. Your custom projects should be added to this solution, not `Orchard.sln` or `DotNest.SDK.sln`.

## Updating your fork with changes from the SDK

- If your repository is an actual fork, you can use the [`Sync fork` feature](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/syncing-a-fork) on GitHub to manually update it.
- Regardless of whether your repository is a fork or not, you can also automate updating its `dev` branch from the SDK using our [Mirror branches workflow](https://github.com/Lombiq/GitHub-Actions/blob/dev/Docs/Workflows/Productivity/MirrorBranches.md). A minimal mirror workflow looks like this (you need to update the `destination-repository` parameter and set up a secret for `DESTINATION_TOKEN`):

    ```yaml
    name: Mirror from SDK

    on:
      workflow_dispatch:
      schedule:
        - cron: '0 0 * * *' # Once every day.

    jobs:
      mirror-from-sdk:
        name: Mirror from SDK
        uses: Lombiq/GitHub-Actions/.github/workflows/mirror-branches.yml@dev
        with:
          source-repository: Lombiq/DotNest-SDK
          destination-repository: AwesomeDeveloper/Awesome-Project
          branch-names: dev
        secrets:
          DESTINATION_TOKEN: ${{ secrets.AWESOME_PROJECT_MIRROR_TOKEN }}
    ```

In case new commits are pushed to your fork from the SDK, check the changes (e.g. new modules might be added that you also need to add to your custom solution) and merge `dev` into your development branch. You can also automate this with the [Open Pull Request workflow](https://github.com/Lombiq/GitHub-Actions/blob/dev/Docs/Workflows/Productivity/OpenPullRequest.md).

## Theme development

- General Orchard theme development rules apply with some DotNest-related extra. You can read about all these on the [`Theming a DotNest site` page of the DotNest Knowledge Base](https://dotnest.com/knowledge-base/topics/theming/).
- If your theme contains Liquid templates, enable the `Liquid Markup View Engine` feature for these to be picked up by Orchard.
- The Media Theme on DotNest also has an automated mechanism to include some site-level resources. This might come in handy e.g. if your theme doesn't have a base theme and/or you're not overriding the `Document` or `Layout` shapes. You can enable the same functionality by enabling the `DotNest SDK` feature, which will automatically include the following resources on every page load (if they are available in the active theme): `favicon.ico` in the `Images` folder, `site.css` in the `Styles` folder, and `site-head.js` and `site-foot.js` in the `Scripts` folder.
- You can synchronize content from your site running on DotNest by exporting it and then importing it locally after enabling the `Import-Export` feature.

## Help us make it better!

In case you come across an Orchard bug during development, don't keep it to yourself: Orchard bugs should be reported at [the official Orchard GitHub repository](https://github.com/OrchardCMS/Orchard). There is a chance though that the problem you've discovered is already fixed since the latest release and we could add the necessary changes as a hotfix to DotNest (and the DotNest SDK) to improve it. You can tell us about it by opening an issue at [the DotNest SDK GitHub repository](https://github.com/Lombiq/DotNest-SDK). Thanks!
