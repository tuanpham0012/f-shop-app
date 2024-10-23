name: Deploy VueJS Project

on:
  push:
    branches:
      - main

jobs:
  build:
    name: Build
    runs-on: ubuntu-latest
    steps:
      - name: Checkout Repo  # Checks out the repository.
        uses: actions/checkout@v4

      - name: Create Node Environment  # Sets up Node.js environment.
        uses: actions/setup-node@v4
        with: 
          node-version: 20.x
      - name: Install Dependencies
        run: npm install
      - name: Build App
        run: npm run build -- --base=/admin-shop-app/
      - name: Setup Pages
        uses: actions/configure-pages@v4
      - name: Archive artifact
        shell: sh
        run: |
          tar \
            --dereference --hard-dereference \
            --directory "dist" \
            -cvf "$RUNNER_TEMP/artifact.tar" \
            --exclude=.git \
            --exclude=.github \
            .
      - name: Upload artifact
        uses: actions/upload-artifact@v4
        with:
          name: github-pages
          path: ${{ runner.temp }}/artifact.tar
          if-no-files-found: error

  deploy:
    name: Deployment
    # Chờ "build" xong mới chạy "deploy"
    needs: build

    # Grant GITHUB_TOKEN the permissions required to make a Pages deployment
    permissions:
      actions: read
      contents: read
      pages: write      # to deploy to Pages
      id-token: write   # to verify the deployment originates from an appropriate source

    # Deploy to the github-pages environment
    environment:
      name: github-pages
      url: ${{ steps.deployment.outputs.page_url }}

    runs-on: ubuntu-latest
    steps:
      - name: Deploy to GitHub Pages
        id: deployment
        uses: actions/deploy-pages@v4